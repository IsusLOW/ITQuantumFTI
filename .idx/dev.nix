{ pkgs, ... }: {
  # Which nixpkgs channel to use.
  channel = "stable-24.05"; # or "unstable"

  # Use https://search.nixos.org/packages to find packages
  packages = [
    pkgs.postgresql
    pkgs.dotnet-sdk_8
  ];

  # Sets environment variables in the workspace
  env = {};

  idx = {
    # Search for the extensions you want on https://open-vsx.org/ and use "publisher.id"
    extensions = [
      "google.gemini-cli-vscode-ide-companion"
    ];

    # Enable previews and define a web preview
    previews = {
      enable = false;
      previews = {
        # This preview will run your services and make the gateway available.
        web = {
          manager = "web";
          # This command starts both of your services.
          # 1. It starts the SliderService in the background on port 5001.
          # 2. It waits until port 5001 is open, indicating the service is ready.
          # 3. It starts the MainApiGateway, binding to the $PORT provided by IDX
          #    so it can be viewed in the preview tab.
          command = [
            "bash"
            "-c"
            "dotnet run --project src/Services/SliderService/SliderService.csproj --urls http://localhost:5001 & while ! (echo > /dev/tcp/localhost/5001) 2>/dev/null; do sleep 0.1; done; dotnet run --project src/Gateways/MainApiGateway/MainApiGateway.csproj --urls http://0.0.0.0:$PORT"
          ];
        };
      };
    };

    # Workspace lifecycle hooks
    workspace = {
      # Runs when a workspace is first created
      onCreate = {
        # Open editors for the following files by default, if they exist:
        default.openFiles = [ ".idx/dev.nix" "README.md" ];
      };
      # Runs when the workspace is (re)started
      # onStart is no longer needed as the command is in the preview definition.
      onStart = {};
    };
  };
}

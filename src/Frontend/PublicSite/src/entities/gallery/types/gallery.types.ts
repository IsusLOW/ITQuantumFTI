export interface GalleryImageDto {
  readonly id: number;
  readonly imageUrl: string;
  readonly title?: string;
  readonly order: number;
  readonly createdAt: string;
}

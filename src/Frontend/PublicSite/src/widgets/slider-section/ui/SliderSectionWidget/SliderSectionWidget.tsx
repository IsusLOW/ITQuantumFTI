import { Slider } from '@/entities/slider/ui/Slider/Slider.js';

interface SliderSectionWidgetProps {
  priority?: boolean;
}

export function SliderSectionWidget({ priority = false }: SliderSectionWidgetProps) {
  return <Slider priority={priority} />;
}

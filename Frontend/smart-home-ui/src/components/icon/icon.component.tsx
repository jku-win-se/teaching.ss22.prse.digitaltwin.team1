import * as Muicon from "@mui/icons-material";
import { SvgIconProps } from "@mui/material";

export const Icon = ({
  name,
  ...rest
}: { name: keyof typeof Muicon } & SvgIconProps) => {
  const IconComponent = Muicon[name];
  return IconComponent ? <IconComponent {...rest} /> : null;
};

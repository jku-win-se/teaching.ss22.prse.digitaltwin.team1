import * as React from "react";
import * as Muicon from "@mui/icons-material";
import { SvgIconProps } from "@mui/material/SvgIcon";
import "./information-panel-item.style.css";
import { Skeleton } from "@mui/material";

export interface IInformationPanelItemProps {
  value: string | number | undefined;
  unit?: string;
  icon: keyof typeof Muicon;
  color?: string;
  isLoading: boolean;
}

const Icon = ({
  name,
  ...rest
}: { name: keyof typeof Muicon } & SvgIconProps) => {
  const IconComponent = Muicon[name];
  return IconComponent ? <IconComponent {...rest} /> : null;
};

export default function InformationPanelItem(
  props: IInformationPanelItemProps
) {
  if (props.isLoading) {
    return (
      <div className="information-panel-wrapper">
        <Skeleton
          animation="wave"
          variant="rectangular"
          width={"2rem"}
          height={"2rem"}
        />
        <p className="information-panel-text">
          <Skeleton
            animation="wave"
            variant="text"
            height={"2rem"}
            width={"5rem"}
          />
        </p>
      </div>
    );
  } else {
    return (
      <div className="information-panel-wrapper">
        <Icon
          sx={{ fontSize: "clamp(1.7rem, 2.5vw, 2.2rem)" }}
          name={props.icon}
        ></Icon>
        <p className="information-panel-text">
          {props.value} {props.unit}
        </p>
      </div>
    );
  }
}

import * as Muicon from "@mui/icons-material";
import { Skeleton } from "@mui/material";
import { Icon } from "../../../../components/icon/icon.component";
import { getCO2Color } from "../../../../utils/getCO2Color";
import "./information-panel-item.style.css";

export interface IInformationPanelItemProps {
  value: string | number | undefined;
  unit?: string;
  icon: keyof typeof Muicon;
  color?: string;
  isLoading: boolean;
  dynamic?: boolean;
  numericValue?: number;
}

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
          sx={{ fontSize: "clamp(1.7rem, 2.5vw, 2.2rem)", color: props.color }}
          name={props.icon}
        ></Icon>
        <p
          className="information-panel-text"
          style={{
            color: props.dynamic
              ? getCO2Color(Number(props.numericValue!))
              : "",
          }}
        >
          {props.value} {props.unit}
        </p>
      </div>
    );
  }
}

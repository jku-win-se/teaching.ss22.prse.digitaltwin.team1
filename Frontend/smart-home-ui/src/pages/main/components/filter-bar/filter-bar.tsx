import { Box, Tab, Tabs } from "@mui/material";
import React from "react";
import { Building } from "../../../../enums/building.enum";

interface IFilterBarProps {
  changeFilterValue(newValue: string): void;
}

export default function FilterBar(props: IFilterBarProps) {
  const [value, setValue] = React.useState("AR");

  const handleChange = (
    event: React.SyntheticEvent,
    newValue: keyof typeof Building
  ) => {
    setValue(newValue);
    props.changeFilterValue(newValue);
  };

  return (
    <div>
      <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
        <Tabs
          value={value}
          onChange={handleChange}
          variant="scrollable"
          scrollButtons
          allowScrollButtonsMobile
          aria-label="scrollable force tabs example"
        >
          {Object.entries(Building).map((val, index) => (
            <Tab key={val[0]} value={val[0]} label={val[1]} />
          ))}
        </Tabs>
      </Box>
    </div>
  );
}

import { Box, Tab, Tabs } from "@mui/material";
import React from "react";
import { EnumType } from "typescript";
import { RoomTypeIcon } from "../../../../enums/roomTypeIcon.enum";

function createTabs(roomTypes: EnumType) {
    for (const value in roomTypes) {
        <Tab label={value} />
    }
}

export default function FilterBar() {
    const [value, setValue] = React.useState('1');

    const handleChange = (event: React.SyntheticEvent, newValue: string) => {
        setValue(newValue);
    };

    return (
        <div>
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs
                    value={value}
                    onChange={handleChange}
                    variant="scrollable"
                    scrollButtons
                    allowScrollButtonsMobile
                    aria-label="scrollable force tabs example"
                >
                    <Tab label="All Rooms" />
                    <Tab label="Science Park 5" />
                    <Tab label="Science Park 2" />
                    <Tab label="Kepler Hall" />
                    createTabs(RoomType)
                </Tabs>
            </Box>
        </div>
    );
}
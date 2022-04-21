import { Box, Grid } from "@mui/material";
import * as React from "react";
import "./details.style.css";

export interface IDetailsProps {}

export function Details(props: IDetailsProps) {
  return (
    <Box sx={{ flexGrow: 1, height: "100vh" }}>
      <Grid container spacing={2}>
        <Grid className="grid-bg" item sm={12} md={6}>
          <h1 className="header-font-size">Dashboard</h1>
          <Grid
            className="infoboard-height"
            item
            xs={10}
            sx={{ margin: "auto" }}
          >
            <div></div>
          </Grid>
          <Grid className="control-height" item xs={10} sx={{ margin: "auto" }}>
            <div></div>
          </Grid>
        </Grid>
        <Grid className="grid-bg" item sm={12} md={6}>
          <Grid item className="chart-height" xs={10}>
            <div></div>
          </Grid>
          <Grid item className="chart-height" xs={10}>
            <div></div>
          </Grid>
          <Grid item className="chart-height" xs={10}>
            <div></div>
          </Grid>
        </Grid>
      </Grid>
    </Box>
  );
}

import { Skeleton } from "@mui/material";
import {
  CategoryScale, Chart as ChartJS, ChartOptions, Legend, LinearScale, LineElement, PointElement, TimeSeriesScale, Title,
  Tooltip
} from "chart.js";
import "chartjs-adapter-date-fns";
import de from "date-fns/locale/de";
import * as React from "react";
import { Line } from "react-chartjs-2";
import { Measure } from "../../../../enums/measure.enum";
import { IChartData } from "../../../../models/IChartData";
import { IRoom } from "../../../../models/IRoom";
import { StateService } from "../../../../services/State.service";

export interface ITempAndCo2ChartProps {
  room: IRoom | undefined;
}
ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  TimeSeriesScale
);
const sService = StateService.getInstance();
export const options: ChartOptions = {
  responsive: true,
  interaction: {
    mode: "index" as const,
    intersect: false,
  },
  plugins: {
    title: {
      display: true,
      text: "Chart.js Line Chart - Multi Axis",
    },
  },
  scales: {
    y: {
      type: "linear" as const,
      display: true,
      position: "left" as const,
    },
    y1: {
      type: "linear" as const,
      display: true,
      position: "right" as const,
      grid: {
        drawOnChartArea: false,
      },
    },
  },
};

export default function TempAndCo2Chart(props: ITempAndCo2ChartProps) {
  const [chartData, setChartData] = React.useState({
    labels: [] as any[],
    datasets: [
      {
        label: "Temperature",
        cubicInterpolationMode: "monotone",
        tension: 0.4,
        data: [] as any[],
        borderColor: "#5BA755",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        pointRadius: 0,
      },
      {
        label: "CO2 Value",
        cubicInterpolationMode: "monotone",
        tension: 0.4,
        data: [] as any[],
        borderColor: "#D95C4C",
        backgroundColor: "rgba(53, 162, 235, 0.5)",
        yAxisID: "y1",
        pointRadius: 0,
      },
    ],
  });

  const [loading, setLoading] = React.useState(true);
  const [intervalId, setIntervalId] = React.useState<NodeJS.Timer>();
  async function fetchData(roomID: string) {
    console.log("Update Temperature");
    let Temperature: IChartData[] = [];
    let Co2: IChartData[] = [];
    try {
      Temperature = await sService.getMeasureChartData(
        roomID,
        Measure.Temperature
      );
    } catch (err) {
      console.log(err);
    }
    try {
      Co2 = await sService.getMeasureChartData(roomID, Measure.Co2);
    } catch (err) {
      console.log(err);
    }

    setChartData({
      labels: Co2.map((val) => new Date(val.timeStamp)),
      datasets: [
        {
          label: "Temperature",
          cubicInterpolationMode: "monotone",
          tension: 0.4,
          data: Temperature.map((val) => val.value),
          borderColor: "#5BA755",
          backgroundColor: "rgba(255, 99, 132, 0.5)",
          yAxisID: "y",
          pointRadius: 0,
        },
        {
          label: "CO2 Value",
          cubicInterpolationMode: "monotone",
          tension: 0.4,
          data: Co2.map((val) => val.value),
          borderColor: "#D95C4C",
          backgroundColor: "rgba(53, 162, 235, 0.5)",
          yAxisID: "y1",
          pointRadius: 0,
        },
      ],
    });
    setLoading(false);
  }
  React.useEffect(() => {
    if (props.room !== undefined) {
      fetchData(props.room.id);
      setIntervalId(
        setInterval(() => {
          fetchData(props.room!.id);
        }, 300000)
      );
    }
  }, [props.room]);

  React.useEffect(() => {
    return () => {
      clearInterval(intervalId!);
    };
  }, []);

  if (loading) {
    return (
      <Skeleton
        style={{ margin: "20px 0" }}
        height={250}
        variant="rectangular"
      ></Skeleton>
    );
  } else {
    return (
      <div style={{ height: "250px", margin: "20px 0" }}>
        <Line
          options={{
            font: {
              family: "Poppins",
              weight: "400",
            },
            responsive: true,
            maintainAspectRatio: false,
            interaction: {
              mode: "index" as const,
              intersect: false,
            },
            plugins: {
              legend: {
                position: "bottom",
                align: "start",
                labels: {
                  boxHeight: 1,
                  boxWidth: 25,
                },
              },
              title: {
                display: true,
                align: "start",
                text: "Temperature and CO2 Values",
                font: {
                  size: 20,
                },
              },
            },
            scales: {
              x: {
                grid: {
                  drawBorder: true,
                  display: false,
                },
                type: "timeseries",
                adapters: {
                  date: {
                    locale: de,
                  },
                },
                ticks: {
                  source: "auto",
                },
                time: {
                  unit: "hour",
                  displayFormats: {
                    hour: "HH:MM",
                  },
                },
              },
              y: {
                grid: {
                  drawBorder: false,
                  display: true,
                },
                title: {
                  text: "Temperature [°C]",
                  display: true,
                },
                type: "linear",
                display: true,
                position: "left" as const,
              },
              y1: {
                title: {
                  text: "CO2 [ppm]",
                  display: true,
                },
                type: "linear",
                display: true,
                position: "right" as const,
                grid: {
                  drawOnChartArea: false,
                },
              },
            },
          }}
          data={chartData as any}
        />
      </div>
    );
  }
}

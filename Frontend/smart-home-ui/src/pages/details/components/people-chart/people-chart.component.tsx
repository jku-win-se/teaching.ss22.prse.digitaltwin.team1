import * as React from "react";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
  ChartOptions,
  TimeSeriesScale,
} from "chart.js";
import { Line } from "react-chartjs-2";
import "chartjs-adapter-date-fns";
import de from "date-fns/locale/de";
import { StateService } from "../../../../services/State.service";
import { Measure } from "../../../../enums/measure.enum";
import { IRoom } from "../../../../models/IRoom";
import { Skeleton } from "@mui/material";
import { IChartData } from "../../../../models/IChartData";

export interface IPeopleChartProps {
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

const sService = StateService.getInstance();

export default function PeopleChart(props: IPeopleChartProps) {
  const [chartData, setChartData] = React.useState({
    labels: [] as any[],
    datasets: [
      {
        label: "amount of people inside",
        data: [] as any[],
        borderColor: "#66B5D6",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        stepped: true,
        pointRadius: 0,
      },
    ],
  });

  const [loading, setLoading] = React.useState(true);
  const [intervalId, setIntervalId] = React.useState<NodeJS.Timer>();
  async function fetchData(roomID: string) {
    console.log("Update PeopleInRoom");
    let PeopleInRoom: IChartData[] = [];
    try {
      PeopleInRoom = await sService.getMeasureChartData(
        roomID,
        Measure.PeopleInRoom
      );
    } catch (err) {
      console.log(err);
    }

    setChartData({
      labels: PeopleInRoom.map((val) => new Date(val.timeStamp)),
      datasets: [
        {
          label: "amount of people inside",
          data: PeopleInRoom.map((val) => val.value),
          borderColor: "#66B5D6",
          backgroundColor: "rgba(255, 99, 132, 0.5)",
          yAxisID: "y",
          stepped: true,
          pointRadius: 0,
        },
      ],
    });
    setLoading(false);
  }
  React.useEffect(() => {
    if (props.room !== undefined) {
      fetchData(props.room.id);
      //clearInterval(intervalId!);
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
                text: "People",
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
                  text: "People",
                  display: true,
                },
                type: "linear",
                display: true,
                position: "left" as const,
              },
            },
          }}
          data={chartData as any}
        />
      </div>
    );
  }
}

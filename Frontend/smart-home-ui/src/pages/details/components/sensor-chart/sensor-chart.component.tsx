import { Skeleton } from "@mui/material";
import {
  CategoryScale,
  Chart as ChartJS,
  ChartOptions,
  Legend,
  LinearScale,
  LineElement,
  PointElement,
  TimeScale,
  TimeSeriesScale,
  Title,
  Tooltip,
} from "chart.js";
import "chartjs-adapter-date-fns";
import de from "date-fns/locale/de";
import * as React from "react";
import { Line } from "react-chartjs-2";
import { Equipment } from "../../../../enums/equipment.enum";
import { IChartData } from "../../../../models/IChartData";
import { IRoom } from "../../../../models/IRoom";
import { RoomService } from "../../../../services/Room.service";
import { StateService } from "../../../../services/State.service";

export interface ISensorChartProps {
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
  TimeSeriesScale,
  TimeScale
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
const rService = RoomService.getInstance();

export default function SensorChart(props: ISensorChartProps) {
  const [loading, setLoading] = React.useState(true);
  const [intervalId, setIntervalId] = React.useState<NodeJS.Timer>();
  const [chartData, setChartData] = React.useState({
    labels: [] as any[],
    datasets: [
      {
        label: "Window(s)",
        data: [] as any[],
        borderColor: "#AE619D",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        stepped: true,
        pointRadius: 0,
      },
      {
        label: "Door(s)",
        data: [] as any[],
        borderColor: "#0084BB",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        stepped: true,
        pointRadius: 0,
      },
      {
        label: "Fan(s)",
        data: [] as any[],
        borderColor: "#BFCE52",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        stepped: true,
        pointRadius: 0,
      },
      {
        label: "Light(s)",
        data: [] as any[],
        borderColor: "#F1BC3F",
        backgroundColor: "rgba(255, 99, 132, 0.5)",
        yAxisID: "y",
        stepped: true,
        pointRadius: 0,
      },
    ],
  });

  const getLabels = (
    WindowsChartData: IChartData[],
    DoorsChartData: IChartData[],
    LightsChartData: IChartData[],
    VentilatorsChartData: IChartData[]
  ) => {
    const arrLenghts = [
      WindowsChartData,
      DoorsChartData,
      LightsChartData,
      VentilatorsChartData,
    ];
    arrLenghts.sort((a, b) => b.length - a.length);
    return arrLenghts[0].map((x) => x.timeStamp);
  };
  async function fetchData() {
    const Doors = rService.getEquipmentByName(Equipment.Door);
    const Ventilators = rService.getEquipmentByName(Equipment.Ventilator);
    const Windows = rService.getEquipmentByName(Equipment.Window);
    const Lights = rService.getEquipmentByName(Equipment.Light);
    let WindowsChartData: IChartData[] = [];
    let DoorsChartData: IChartData[] = [];
    var VentilatorsChartData: IChartData[] = [];
    var LightsChartData: IChartData[] = [];
    if (Doors.length !== 0) {
      if (Doors[0].state.length !== 0) {
        DoorsChartData = await sService.getBinaryChartData(
          Doors.map((d) => d.id),
          Doors[0].state[0].name
        );
      }
    }
    if (Windows.length !== 0) {
      if (Windows[0].state.length !== 0) {
        WindowsChartData = await sService.getBinaryChartData(
          Windows.map((w) => w.id),
          Windows[0].state[0].name
        );
      }
    }
    if (Lights.length !== 0) {
      if (Lights[0].state.length !== 0) {
        LightsChartData = await sService.getBinaryChartData(
          Lights.map((l) => l.id),
          Lights[0].state[0].name
        );
      }
    }
    if (Ventilators.length !== 0) {
      if (Ventilators[0].state.length !== 0) {
        VentilatorsChartData = await sService.getBinaryChartData(
          Ventilators.map((v) => v.id),
          Ventilators[0].state[0].name
        );
      }
    }
    setChartData({
      labels: getLabels(
        WindowsChartData,
        DoorsChartData,
        LightsChartData,
        VentilatorsChartData
      ),
      datasets: [
        {
          label: "Window(s)",
          data: WindowsChartData.map((w) => w.value),
          borderColor: "#AE619D",
          backgroundColor: "rgba(255, 99, 132, 0.5)",
          yAxisID: "y",
          stepped: true,
          pointRadius: 0,
        },
        {
          label: "Door(s)",
          data: DoorsChartData.map((d) => d.value),
          borderColor: "#0084BB",
          backgroundColor: "rgba(255, 99, 132, 0.5)",
          yAxisID: "y",
          stepped: true,
          pointRadius: 0,
        },
        {
          label: "Fan(s)",
          data: VentilatorsChartData.map((v) => v.value),
          borderColor: "#BFCE52",
          backgroundColor: "rgba(255, 99, 132, 0.5)",
          yAxisID: "y",
          stepped: true,
          pointRadius: 0,
        },
        {
          label: "Light(s)",
          data: LightsChartData.map((l) => l.value),
          borderColor: "#F1BC3F",
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
      fetchData();
      setIntervalId(
        setInterval(() => {
          fetchData();
        }, 300000)
      );
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.room]);
  React.useEffect(() => {
    return () => {
      clearInterval(intervalId!);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
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
                text: "Window, door, fan and light state",
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
                type: "time",
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
                  text: "open/on",
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
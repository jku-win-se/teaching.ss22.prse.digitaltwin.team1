import { IRoom } from "../../../../models/IRoom";
import RoomListItem from "../room-list-item/room-list-item.component";
import "./room-list.styles.css";

interface IRoomListProps {
  rooms: IRoom[];
  triggerReload: () => void;
}

export default function RoomList(props: IRoomListProps) {
  return (
    <div>
      {props.rooms.map((room) => (
        <RoomListItem
          key={room.id}
          triggerReload={props.triggerReload}
          roomId={room.id}
          roomName={room.name}
          roomIcon={room.roomType}
          building={room.building}
          maxPeople={room.peopleCount}
        />
      ))}
    </div>
  );
}

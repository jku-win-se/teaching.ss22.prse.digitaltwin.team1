import RoomListItem from "../room-list-item/room-list-item.component";
import "./room-list.styles.css";

export interface IRoomListProps { }

export default function RoomList(props: IRoomListProps) {
    return (
        <div >
            <RoomListItem
                roomName="S5 103"
                roomIcon="ScienceOutlined"
                building="Science Park 5"
                coValue={500}
                currentPeople={23}
                maxPeople={50}
            />

            <RoomListItem
                roomName="S5 102"
                roomIcon="MeetingRoomOutlined"
                building="Science Park 5"
                coValue={900}
                currentPeople={6}
                maxPeople={40}
            />

            <RoomListItem
                roomName="HS 18"
                roomIcon="SchoolOutlined"
                building="Kepler Hall"
                coValue={1700}
                currentPeople={0}
                maxPeople={150}
            />
        </div>
    );
}
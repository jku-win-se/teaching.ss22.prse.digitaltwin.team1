import RoomListItem from "../room-list-item/room-list-item.component";
import "./room-list.styles.css";

export default function RoomList() {
    return (
        <div >
            <RoomListItem
                roomId="3fa85f64-5717-4562-b3fc-2c963f66afa6"
                roomName="S5 103"
                roomIcon="Lab"
                building="Science Park 5"
                coValue={500}
                currentPeople={23}
                maxPeople={50}
            />

            <RoomListItem
                roomId=""
                roomName="S5 102"
                roomIcon="MeetingRoom"
                building="Science Park 5"
                coValue={900}
                currentPeople={6}
                maxPeople={40}
            />

            <RoomListItem
                roomId=""
                roomName="HS 18"
                roomIcon="LectureRoom"
                building="Kepler Hall"
                coValue={1700}
                currentPeople={0}
                maxPeople={150}
            />
        </div>
    );
}
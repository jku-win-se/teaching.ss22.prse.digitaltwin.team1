import React from "react";
import { Measure } from "../../../../enums/measure.enum";
import { IRoom } from "../../../../models/IRoom";
import { StateService } from "../../../../services/State.service";
import RoomListItem from "../room-list-item/room-list-item.component";
import "./room-list.styles.css";

interface IRoomListProps { rooms: IRoom[] }

export default function RoomList(props: IRoomListProps) {
    return (
        <div >
            {props.rooms.map((room, idx, arr) => (
                <RoomListItem
                    key={room.id}
                    roomId={room.id}
                    roomName={room.name}
                    roomIcon={room.roomType}
                    building={room.building}
                    //coValue={500}
                    //currentPeople={25}
                    maxPeople={room.peopleCount}
                />
            ))}
        </div>
    );
}
import { RoomType } from "../enums/roomType.enum";
import { RoomTypeIcon } from "../enums/roomTypeIcon.enum";

export function getRoomTypeIcon(type: keyof typeof RoomType) {
  if (type === "Lab") {
    return RoomTypeIcon.Lab;
  }
  if (type === "LectureHall") {
    return RoomTypeIcon.LectureRoom;
  }
  return RoomTypeIcon.MeetingRoom;
}

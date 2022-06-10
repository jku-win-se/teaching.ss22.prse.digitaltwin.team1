export function getCO2Color(value: number) {
  if (isNaN(value)) {
    return "#000000";
  }
  if (value < 800) {
    return "#71CCAB";
  }
  if (value > 1000) {
    return "#FF5252";
  }
  return "#FFEE4D";
}

export async function asyncForEach<T>(
  array: T[],
  callback: (val: T, index: number, array: T[]) => void
) {
  for (let index = 0; index < array.length; index++) {
    await callback(array[index], index, array);
  }
}

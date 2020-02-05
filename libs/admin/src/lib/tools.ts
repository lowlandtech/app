export const hasClass = (target: any, elementClassName: string) => {
  return new RegExp('(\\s|^)' + elementClassName + '(\\s|$)').test(target.className);
};

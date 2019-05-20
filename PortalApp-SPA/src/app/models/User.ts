export interface User {
  id: number;
  UserName: string;
  FullName: string;
  team?: string[];
  roles?: string[];
}

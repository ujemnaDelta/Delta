export interface User {
  id: number;
  UserName: string;
  FullName: string;
  Password: string;
  team?: string[];
  roles?: string[];
}

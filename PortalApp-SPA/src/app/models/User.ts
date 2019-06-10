export interface User {
  id: number;
  userName: string;
  fullUserName: string;
  UserPassword: string;
  team?: string[];
  roles?: string[];
  position: string;
}

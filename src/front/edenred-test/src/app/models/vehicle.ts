export interface Vehicle {
  id: number;
  clientId: number | null;
  clientName: string | null;
  licensePlate: string;
  manufactureYear: number;
  brand: string;
  model: string;
}

import { Company } from './company';

export interface Project{
    id: string;
    name: string;
    description: string;
    startDate: Date;
    endDate: Date;
}
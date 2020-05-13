import { Company } from './company';

export class Project {
    public constructor(init?: Partial<Project>) {
        Object.assign(this, init);
    }

    Id: string;
    Name: string;
    Description: string;
    StartDate: Date;
    EndDate: Date;
}
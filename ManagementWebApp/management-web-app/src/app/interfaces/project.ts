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

export interface Project {
    Id: string;
    Name: string;
    Description: string;
    StartDate: Date;
    EndDate: Date;
}
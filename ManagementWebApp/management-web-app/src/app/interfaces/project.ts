import { Team } from './team';
export class Project {
    public constructor(init?: Partial<Project>) {
        Object.assign(this, init);
    }

    Id: string;
    Name: string;
    Description: string;
    StartDate: Date;
    EndDate: Date;
    TeamsOnProject: Array<Team>;
}

export interface Project {
    Id: string;
    Name: string;
    Description: string;
    StartDate: Date;
    EndDate: Date;
    TeamsOnProject: Array<Team>;
}
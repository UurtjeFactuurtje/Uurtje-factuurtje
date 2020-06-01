import { People } from './people';
export class Team {
    public constructor(init?: Partial<Team>) {
        Object.assign(this, init);
    }

    Id: string;
    Name: string;
    EmployeesInTeam: Array<People>;
}

export interface Team {
    Id: string;
    Name: string;
    EmployeesInTeam: Array<People>;
}
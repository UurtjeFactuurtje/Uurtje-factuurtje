export class People {
    public constructor(init?: Partial<People>) {
        Object.assign(this, init);
    }

    Id: string;
    FirstName: string;
    LastName: string;
}

export interface People {
    Id: string;
    FirstName: string;
    LastName: string;
}
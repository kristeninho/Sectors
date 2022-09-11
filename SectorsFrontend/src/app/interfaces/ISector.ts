export interface ISector{
    sector: Sector,
    sectors?: ISector[]
}

interface Sector{
    hasSubSectors: boolean,
    id: number,
    level: number,
    name: string,
    parentId: number
}
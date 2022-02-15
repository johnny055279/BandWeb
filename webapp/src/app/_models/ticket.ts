export interface Ticket {
    id: number,
    showTime: Date,
    location: string,
    price: number,
    title: string,
    subTitle: string,
    remainNumber: number,
    soldOut: boolean,
    completeShow: boolean,
    open: boolean,
    imageUrl: string,
}

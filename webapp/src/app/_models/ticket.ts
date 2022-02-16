export interface Ticket {
    id: string,
    showTime: Date,
    cityId: string,
    cityName: string,
    price: number,
    title: string,
    subTitle: string,
    remainNumber: number,
    soldOut: boolean,
    completeShow: boolean,
    open: boolean,
    imageUrl: string,
    purchaseDeadLine: Date
}

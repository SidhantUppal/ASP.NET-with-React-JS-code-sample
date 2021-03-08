import { Images } from '../definitions'

export interface ISizeDescriptor {
    id: string
    title: string
    image: keyof typeof Images
    capacity: number
    price: number
}
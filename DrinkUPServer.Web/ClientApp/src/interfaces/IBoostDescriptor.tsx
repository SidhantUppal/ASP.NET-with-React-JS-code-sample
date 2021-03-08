import { Images } from '../definitions'

export interface IBoostDescriptor {
    id: string
    title: string
    subtitle: string
    image: keyof typeof Images
    price: number
    details: string
}
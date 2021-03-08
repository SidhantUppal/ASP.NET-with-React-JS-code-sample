import { IBoostDescriptor, ISizeDescriptor } from '../interfaces'

let codes: {
    sizes: Array<ISizeDescriptor>,
    boosts: Array<IBoostDescriptor>,
    payment: any
} = {
    sizes: [
        {
            id: "mach-one-size-small",
            title: "Small",
            image: "SizeSmall",
            capacity: 16,
            price: 0.75
        },
        {
            id: "mach-one-size-medium",
            title: "Medium",
            image: "SizeMedium",
            capacity: 26,
            price: 1.00
        },
        {
            id: "mach-one-size-large",
            title: "Large",
            image: "SizeLarge",
            capacity: 40,
            price: 1.50
        }
    ],
    boosts: [
        {
            id: "mach-one-boost-electrolyte",
            title: "Electrolyte",
            subtitle: "kiwi strawberry",
            image: "BoostElectrolyte",
            price: 0.75,
            details: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        },
        {
            id: "mach-one-boost-protein",
            title: "Protein",
            subtitle: "mixed berry",
            image: "BoostProtein",
            price: 0.75,
            details: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        },
        {
            id: "mach-one-boost-energy",
            title: "Energy",
            subtitle: "cranberry grape",
            image: "BoostEnergy",
            price: 0.75,
            details: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        },
        {
            id: "mach-one-boost-immunity",
            title: "Immunity",
            subtitle: "black cherry",
            image: "BoostImmunity",
            price: 0.75,
            details: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        },
    ],
    payment: [
        {
            name: "paypal"
        },
        {
            name: "credit card"
        },
        {
            name: "mobile"
        }
    ],
}

export default codes
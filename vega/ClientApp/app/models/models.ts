export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    isRegistered: boolean;
    features: Array<KeyValuePair>;
    contact: Contact;
    lastUpdate: Date
}

export interface SaveVehicle {
    id: number;
    modelId: number;
    makeId: number;
    isRegistered: boolean;
    features: Array<number>;
    contact: Contact;
}

export interface Contact {
    name: string;
    phone: string;
    email: string;
}

export interface KeyValuePair {
    id: number;
    name: string;
}
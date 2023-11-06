export interface MessageResponseDTO {
    id: string;
    text: string;
    image: string | null;
    chatId: string;
    senderId: string;
    senderLogo: string;
    dateTime: Date; 
}
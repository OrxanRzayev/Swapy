export interface ChatMessageModel {
    chatId: string;
    recepientId: string;
    senderId: string;
    senderName: string;
    message: string;
    image: string;
    dateTime: Date;
}
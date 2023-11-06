export interface ChatResponseDTO {
    chatId: string;
    title: string;
    logo: string;
    image: string;
    isReaded: boolean;
    lastMessage: string;
    lastMessageDateTime: Date | null;
}
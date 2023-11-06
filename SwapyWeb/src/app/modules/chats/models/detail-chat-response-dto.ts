import { ChatType } from "../enums/chat-type.enum";
import { MessageResponseDTO } from "./message-response-dto";

export interface DetailChatResponseDTO {
    chatId: string;
    title: string;
    image: string;
    messages: MessageResponseDTO[];
    type: ChatType;
}
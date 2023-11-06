import { ChatResponseDTO } from "./chat-response-dto";

export interface ChatListResponseDTO {
    items: ChatResponseDTO[];
    count: number;
}
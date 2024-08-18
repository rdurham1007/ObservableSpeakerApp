import { Speaker } from "../speakers/speaker";

export interface EditTalk {
    title: string;
    abstract: string;
    speaker: Speaker;
}
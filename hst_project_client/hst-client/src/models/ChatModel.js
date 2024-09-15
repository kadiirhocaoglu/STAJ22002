export class ChatModel {
    constructor(userId = "", toUserId = "", date = "", message = "") {
      this.userId = userId;
      this.toUserId = toUserId;
      this.date = date;
      this.message = message;
    }   
  }
  
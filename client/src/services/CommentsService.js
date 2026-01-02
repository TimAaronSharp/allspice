import { AppState } from "@/AppState.js"
import { api } from "./AxiosService.js"
import { logger } from "@/utils/Logger.js"
import { Comment } from "@/models/Comment.js"


class CommentsService{
async create(commentData){
  AppState.activeComment = null
  const res = await api.post("api/comments", commentData)
  logger.log ("CommentsService.create() returned ", res.data)
  this.makeComments(commentData);
  }

  makeComments(commentData){
    if (commentData == null || commentData == "") {
      return
    }
    if (Array.isArray(commentData)) {
      return AppState.comments = commentData.map(pojo => new Comment(pojo));
    }
    return AppState.activeComment = new Comment(commentData);
  }
}

export const commentsService = new CommentsService()
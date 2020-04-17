import { createAction, props } from '@ngrx/store';
import { CardData, CardComment, Profile } from '@lowlandtech/api';

export const loadCard = createAction('[card] LOAD_ARTICLE', props<{ slug: string }>());

export const loadCardSuccess = createAction('[card] LOAD_ARTICLE_SUCCESS', props<{ card: CardData }>());

export const loadCardFail = createAction('[card] LOAD_ARTICLE_FAIL', props<{ error: Error }>());

export const deleteCard = createAction('[card] DELETE_ARTICLE', props<{ slug: string }>());

export const deleteCardFail = createAction('[card] DELETE_ARTICLE_FAIL', props<{ error: Error }>());

export const initializeCard = createAction('[card] INITIALIZE_ARTICLE');

export const loadComments = createAction('[card] LOAD_COMMENTS', props<{ slug: string }>());

export const loadCommentsSuccess = createAction(
  '[card] LOAD_COMMENTS_SUCCESS',
  props<{ comments: CardComment[] }>(),
);

export const loadCommentsFail = createAction('[card] LOAD_COMMENTS_FAIL', props<{ error: Error }>());

export const favorite = createAction('[card] FAVORITE', props<{ slug: string }>());

export const favoriteSuccess = createAction('[card] FAVORITE_SUCCESS', props<{ card: CardData }>());

export const favoriteFail = createAction('[card] FAVORITE_FAIL', props<{ error: Error }>());

export const unFavorite = createAction('[card] UNFAVORITE', props<{ slug: string }>());

export const unFavoriteSuccess = createAction('[card] UNFAVORITE_SUCCESS', props<{ card: CardData }>());

export const unFavoriteFail = createAction('[card] UNFAVORITE_FAIL', props<{ error: Error }>());

export const follow = createAction('[card] FOLLOW', props<{ username: string }>());

export const followSuccess = createAction('[card] FOLLOW_SUCCESS', props<{ profile: Profile }>());

export const followFail = createAction('[card] FOLLOW_FAIL', props<{ error: Error }>());

export const unFollow = createAction('[card] UNFOLLOW', props<{ username: string }>());

export const unFollowSuccess = createAction('[card] UNFOLLOW_SUCCESS', props<{ profile: Profile }>());

export const unFollowFail = createAction('[card] UNFOLLOW_FAIL', props<{ error: Error }>());

export const addComment = createAction('[card] ADD_COMMENT', props<{ slug: string }>());

export const addCommentFail = createAction('[card] ADD_COMMENT_FAIL', props<{ error: Error }>());

export const addCommentSuccess = createAction('[card] ADD_COMMENT_SUCCESS', props<{ comment: CardComment }>());

export const deleteComment = createAction('[card] DELETE_COMMENT', props<{ commentId: number; slug: string }>());

export const deleteCommentFail = createAction('[card] DELETE_COMMENT_FAIL', props<{ error: Error }>());

export const deleteCommentSuccess = createAction('[card] DELETE_COMMENT_SUCCESS', props<{ commentId: number }>());

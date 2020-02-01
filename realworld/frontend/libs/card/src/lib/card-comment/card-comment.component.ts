import { Component, OnInit, Input, ChangeDetectionStrategy, EventEmitter, Output } from '@angular/core';
import { ArticleComment, ArticleData, User } from '@spotacard/api';

@Component({
  selector: 'app-card-comment',
  templateUrl: './card-comment.component.html',
  styleUrls: ['./card-comment.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ArticleCommentComponent {
  @Input() currentUser: User;
  @Input() comment: ArticleComment;
  @Input() card: ArticleData;
  @Output() delete: EventEmitter<{
    commentId: number;
    slug: string;
  }> = new EventEmitter();
}

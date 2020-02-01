import { Component, OnInit, Input, ChangeDetectionStrategy, EventEmitter, Output } from '@angular/core';
import { CardComment, CardData, User } from '@spotacard/api';

@Component({
  selector: 'app-card-comment',
  templateUrl: './card-comment.component.html',
  styleUrls: ['./card-comment.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CardCommentComponent {
  @Input() currentUser: User;
  @Input() comment: CardComment;
  @Input() card: CardData;
  @Output() delete: EventEmitter<{
    commentId: number;
    slug: string;
  }> = new EventEmitter();
}

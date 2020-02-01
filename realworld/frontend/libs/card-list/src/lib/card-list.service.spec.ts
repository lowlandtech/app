import { ApiService } from '@spotacard/api';
import { inject, TestBed } from '@angular/core/testing';

import { ArticleListService } from './card-list.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ArticleListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ArticleListService, ApiService],
    });
  });

  it('should be created', inject([ArticleListService], (service: ArticleListService) => {
    expect(service).toBeTruthy();
  }));
});

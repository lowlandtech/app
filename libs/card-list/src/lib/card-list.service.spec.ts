import { ApiService } from '@lowlandtech/api';
import { inject, TestBed } from '@angular/core/testing';

import { CardListService } from './card-list.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CardListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CardListService, ApiService],
    });
  });

  it('should be created', inject([CardListService], (service: CardListService) => {
    expect(service).toBeTruthy();
  }));
});

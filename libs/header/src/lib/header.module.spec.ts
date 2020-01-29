import { async, TestBed } from '@angular/core/testing';
import { HeaderModule } from './header.module';

describe('HeaderModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HeaderModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(HeaderModule).toBeDefined();
  });
});

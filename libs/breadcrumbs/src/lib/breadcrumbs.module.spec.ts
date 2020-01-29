import { async, TestBed } from '@angular/core/testing';
import { BreadcrumbsModule } from './breadcrumbs.module';

describe('BreadcrumbsModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [BreadcrumbsModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(BreadcrumbsModule).toBeDefined();
  });
});

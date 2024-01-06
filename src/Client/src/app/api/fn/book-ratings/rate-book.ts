/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { RatingDto } from '../../models/rating-dto';

export interface RateBook$Params {
      body?: RatingDto
}

export function rateBook(http: HttpClient, rootUrl: string, params?: RateBook$Params, context?: HttpContext): Observable<StrictHttpResponse<RatingDto>> {
  const rb = new RequestBuilder(rootUrl, rateBook.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'text/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<RatingDto>;
    })
  );
}

rateBook.PATH = '/api/BookRatings';

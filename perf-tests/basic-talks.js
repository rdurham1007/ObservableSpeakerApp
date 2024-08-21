/*
 This is a basic performance test for the bff service
 that uses the k6 library to simulate 100 users making calls to /api/talks
 which sends rmq messages to query the talks
 */

import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 100,
  duration: '60s',
  insecureSkipTLSVerify: true,
};

export default function () {
  http.get('http://localhost:7208/api/talks');
  //sleep(1);
}
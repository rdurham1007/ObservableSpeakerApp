import http from 'k6/http';

export const options = {
  vus: 10,
  duration: '30s',
  insecureSkipTLSVerify: true
};

export default function () {
  http.get('http://localhost:7208/api/speakers');
}
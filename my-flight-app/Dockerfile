FROM node:lts-alpine
WORKDIR /app
COPY ["**/package.json", "**/package-lock.json*", "./"]
COPY **/ ./
RUN npm i
EXPOSE 3000
CMD ["npm", "run", "start"]


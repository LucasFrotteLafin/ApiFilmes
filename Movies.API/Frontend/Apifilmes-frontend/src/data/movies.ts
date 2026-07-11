export interface Movie {
  id: number
  title: string
  poster: string
  description: string
}

const movies: Movie[] = [
  {
    id: 1,
    title: 'Interestelar',
    poster: 'https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg',
    description: 'Um grupo de exploradores viaja por um buraco de minhoca no espaço em busca de um novo lar para a humanidade.',
  },
  {
    id: 2,
    title: 'O Poderoso Chefão',
    poster: 'https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsLeMMovrI8i1.jpg',
    description: 'O patriarca envelhecido de uma dinastia do crime organizado transfere o controle de seu império para seu filho relutante.',
  },
  {
    id: 3,
    title: 'Clube da Luta',
    poster: 'https://image.tmdb.org/t/p/w500/pB8BM7pdSp6B6Ih7QZ4DrQ3PmJK.jpg',
    description: 'Um insone e um vendedor de sabão formam um clube de luta clandestino que evolui para algo muito maior.',
  },
  {
    id: 4,
    title: 'Matrix',
    poster: 'https://image.tmdb.org/t/p/w500/f89U3ADr1oiB1s9GkdPOEpXUk5H.jpg',
    description: 'Um hacker descobre que a realidade que conhece é uma simulação e se junta à resistência contra as máquinas.',
  },
  {
    id: 5,
    title: 'Parasita',
    poster: 'https://image.tmdb.org/t/p/w500/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg',
    description: 'A família Ki-taek, sem emprego e sem perspectivas, se infiltra na vida de uma família rica de Seul.',
  },
  {
    id: 6,
    title: 'Coringa',
    poster: 'https://image.tmdb.org/t/p/w500/udDclJoHjfjb8Ekgsd4FDteOkCU.jpg',
    description: 'Um comediante fracassado de Gotham City desce à loucura e se torna o icônico vilão Coringa.',
  },
]

export default movies

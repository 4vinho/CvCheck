# Repository Guidelines

## Project Structure & Module Organization
This frontend lives in `src/frontend` and is a Vite + Vue 3 + TypeScript app. Source files are under `src/`: `app/router` holds route setup, `layouts` contains shell components, `pages` contains route-level views, `components/shared` contains app-specific UI, `components/ui` contains reusable primitives, `composables` contains Vue Query hooks and UI-facing state orchestration, `services` contains feature-oriented API services, `lib/api` contains shared HTTP/config/error helpers, `lib` contains other helpers, and `styles/globals.css` is the Tailwind entry point. Build output is generated in `dist/` and should not be edited manually.

## Build, Test, and Development Commands
Install dependencies with `npm install`.

- `npm run dev` starts the Vite dev server for local development.
- `npm run build` runs Vue and Node TypeScript checks, then creates the production bundle in `dist/`.
- `npm run typecheck` runs the same static checks without emitting files.
- `npm run preview` serves the last production build locally.

Run `npm run typecheck` before opening a PR, and use `npm run build` for release-facing changes.

## Coding Style & Naming Conventions
Use TypeScript, Vue Single File Components, and 2-space indentation in templates, scripts, and styles. Prefer double quotes, matching the existing codebase. Import app modules through the `@/` alias instead of long relative paths.

Use `PascalCase` for Vue component filenames (`AppHeader.vue`, `NotFoundPage.vue`), `camelCase` for variables and functions, and kebab-case route names only when exposed as router identifiers. Keep low-level reusable primitives in `src/components/ui`; keep feature-specific composition in `pages`, `layouts`, or `components/shared`.

All user-facing product copy must be written in English. This includes page titles, labels, placeholders, helper text, alerts, toasts, validation messages, empty states, navigation labels, and public-facing route paths. Do not introduce Portuguese text in the UI.

For remote data, use `@tanstack/vue-query` as the default pattern:

- `pages` and UI components must not call `fetch` or backend endpoints directly.
- Put HTTP and contract handling in `services/<feature>/`.
- Put `useQuery` / `useMutation` wrappers in `composables/<feature>/`.
- Keep the flow `page/component -> composable -> service -> lib/api client`.
- Reuse shared HTTP helpers from `src/lib/api` instead of creating ad hoc request code.
- For any icon in the system, use `lucide-vue-next`. Do not inline raw SVGs in pages or components unless there is a documented exception.

## Testing Guidelines
There is no test runner configured yet. Until one is added, treat `npm run typecheck` and `npm run build` as the minimum validation for every change. When adding tests later, place them next to the feature or under a dedicated `src/__tests__/` tree and use `*.spec.ts` or `*.test.ts` naming consistently.

## Commit & Pull Request Guidelines
Recent history follows Conventional Commits, for example `feat(frontend): cria baseline vue` and `fix(frontend): evita emit no build`. Keep the format `type(scope): summary`, with scopes such as `frontend`, `repo`, or the affected module.

Pull requests should include a short description, linked issue or task when available, and screenshots or recordings for UI changes. Note any config changes, new commands, or follow-up work directly in the PR body.

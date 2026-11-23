namespace EfcRepositories;

// Your local Postgres connection details live here as a plain constant, same idea
// as the assignment's own suggestion for the SQLite absolute-path problem: commit
// this file once with a harmless placeholder (so the project still builds for
// anyone who clones it), then edit it locally with your real username/password -
// but don't commit or push it again after that, so your real credentials never
// end up on GitHub. "git update-index --skip-worktree" on this file is a nice way
// to stop Rider from ever showing it as changed again, if you want that.
public static class PostgresConnectionSettings
{
    public const string ConnectionString =
        "Host=localhost;Port=5432;Database=dnpassignment1;Username=postgres;Password=mike";
}

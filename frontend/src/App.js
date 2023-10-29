// Common imports
import { Routes, Route } from "react-router-dom";
import AdminRoute from "./utils/AdminRoute";

// Guest imports
import Home from "./pages/Guest/Home";
import Article from "./pages/Guest/Article";
import Players from "./pages/Guest/Players";
import Matches from "./pages/Guest/Matches";
import Table from "./pages/Guest/Table";
import Staff from "./pages/Guest/StaffMembers";
import History from "./pages/Guest/History";
import Trophies from "./pages/Guest/Trophies";

// Admin imports
import Login from "./pages/Admin/Login";
import Admin from "./pages/Admin/Admin";
import AdminPlayers from "./pages/Admin/Players/AdminPlayers";
import AddPlayer from "./pages/Admin/Players/AddPlayer";
import EditPlayer from "./pages/Admin/Players/EditPlayer";
import AdminArticles from "./pages/Admin/Articles/AdminArticles";
import AddArticle from "./pages/Admin/Articles/AddArticle";
import EditArticle from "./pages/Admin/Articles/EditArticle";
import AdminStaffMembers from "./pages/Admin/StaffMembers/AdminStaffMembers";
import AddStaffMember from "./pages/Admin/StaffMembers/AddStaffMember";
import EditStaffMember from "./pages/Admin/StaffMembers/EditStaffMember";
import AdminTeams from "./pages/Admin/Teams/AdminTeams";
import AddTeam from "./pages/Admin/Teams/AddTeam";
import EditTeam from "./pages/Admin/Teams/EditTeam";
import AdminCompetitions from "./pages/Admin/Competitions/AdminCompetitions";
import AddCompetition from "./pages/Admin/Competitions/AddCompetition";
import EditCompetition from "./pages/Admin/Competitions/EditCompetition";
import AdminCompetitionSeasons from "./pages/Admin/CompetitionSeasons/AdminCompetitionSeasons";
import AddCompetitionSeason from "./pages/Admin/CompetitionSeasons/AddCompetitionSeason";
import AdminCompetitionSeasonTeams from "./pages/Admin/CompetitionSeasonTeams/AdminCompetitionSeasonTeams";
import AddCompetitionSeasonTeam from "./pages/Admin/CompetitionSeasonTeams/AddCompetitionSeasonTeam";
import AdminCompetitionSeasonMatches from "./pages/Admin/CompetitionSeasonMatches/AdminCompetitionSeasonMatches";
import AddCompetitionSeasonMatch from "./pages/Admin/CompetitionSeasonMatches/AddCompetitionSeasonMatch";
import EditCompetitionSeasonMatch from "./pages/Admin/CompetitionSeasonMatches/EditCompetitionSeasonMatch";
import AdminHistory from "./pages/Admin/History/AdminHistory";
import AdminTrophies from "./pages/Admin/Trophies/AdminTrophies";
import AddTrophy from "./pages/Admin/Trophies/AddTrophy";

function App() {
  return (
    <Routes>
      {/* Bad URL -> home */}
      <Route path="*" element={<Home />} />

      {/* Guest routes */}
      <Route path="/" element={<Home />} />
      <Route path="/home/page/:page" element={<Home />} />
      <Route path="/article/:articleId" element={<Article />} />
      <Route path="/players" element={<Players />} />
      <Route path="/staff" element={<Staff />} />
      <Route path="/matches" element={<Matches />} />
      <Route path="/table" element={<Table />} />
      <Route path="/history" element={<History />} />
      <Route path="/trophies" element={<Trophies />} />

      {/* Login route */}
      <Route path="/login" element={<Login />} />

      {/* Admin routes */}
      <Route path="/admin" element={<AdminRoute element={<Admin />} />} />
      <Route
        path="/admin/players"
        element={<AdminRoute element={<AdminPlayers />} />}
      />
      <Route
        path="/admin/players/add"
        element={<AdminRoute element={<AddPlayer />} />}
      />
      <Route
        path="/admin/players/edit/:playerId"
        element={<AdminRoute element={<EditPlayer />} />}
      />
      <Route
        path="/admin/articles"
        element={<AdminRoute element={<AdminArticles />} />}
      />
      <Route
        path="/admin/articles/add"
        element={<AdminRoute element={<AddArticle />} />}
      />
      <Route
        path="/admin/articles/edit/:articleId"
        element={<AdminRoute element={<EditArticle />} />}
      />
      <Route
        path="/admin/staff"
        element={<AdminRoute element={<AdminStaffMembers />} />}
      />
      <Route
        path="/admin/staff/add"
        element={<AdminRoute element={<AddStaffMember />} />}
      />
      <Route
        path="/admin/staff/edit/:staffMemberId"
        element={<AdminRoute element={<EditStaffMember />} />}
      />
      <Route
        path="/admin/teams"
        element={<AdminRoute element={<AdminTeams />} />}
      />
      <Route
        path="/admin/teams/add"
        element={<AdminRoute element={<AddTeam />} />}
      />
      <Route
        path="/admin/teams/edit/:teamId"
        element={<AdminRoute element={<EditTeam />} />}
      />
      <Route
        path="/admin/competitions"
        element={<AdminRoute element={<AdminCompetitions />} />}
      />
      <Route
        path="/admin/competitions/add"
        element={<AdminRoute element={<AddCompetition />} />}
      />
      <Route
        path="/admin/competitions/edit/:competitionId"
        element={<AdminRoute element={<EditCompetition />} />}
      />
      <Route
        path="/admin/competition/:competitionId/seasons"
        element={<AdminRoute element={<AdminCompetitionSeasons />} />}
      />
      <Route
        path="/admin/competition/:competitionId/seasons/add"
        element={<AdminRoute element={<AddCompetitionSeason />} />}
      />
      <Route
        path="/admin/competition-season/:competitionSeasonId/teams"
        element={<AdminRoute element={<AdminCompetitionSeasonTeams />} />}
      />
      <Route
        path="/admin/competition-season/:competitionSeasonId/teams/add"
        element={<AdminRoute element={<AddCompetitionSeasonTeam />} />}
      />
      <Route
        path="/admin/competition-season/:competitionSeasonId/matches"
        element={<AdminRoute element={<AdminCompetitionSeasonMatches />} />}
      />
      <Route
        path="/admin/competition-season/:competitionSeasonId/matches/add"
        element={<AdminRoute element={<AddCompetitionSeasonMatch />} />}
      />
      <Route
        path="/admin/competition-season/:competitionSeasonId/match/:matchId/edit"
        element={<AdminRoute element={<EditCompetitionSeasonMatch />} />}
      />
      <Route
        path="/admin/history"
        element={<AdminRoute element={<AdminHistory />} />}
      />
      <Route
        path="/admin/trophies"
        element={<AdminRoute element={<AdminTrophies />} />}
      />
      <Route
        path="/admin/trophy/add"
        element={<AdminRoute element={<AddTrophy />} />}
      />
    </Routes>
  );
}

export default App;
